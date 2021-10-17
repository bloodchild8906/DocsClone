using DocsClone.Domain.Entities;
using DocsClone.Domain.Interfaces;
using DocsClone.Dto.V1.Document.Request;
using DocsClone.Dto.V1.Document.Response;
using DocsClone.Dto.V1.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocsClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DocumentController(IUnitOfWork unitOfWork, IConfiguration configuration) => _unitOfWork = unitOfWork;
        


        [Route("List")]
        [HttpGet]
        public ActionResult List()
        {
            var currentUser = HttpContext.User;
            var username = currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == username).FirstOrDefault();
            var docs = tmpUser.Documents;
            var ret = new List<DocumentResponse>();
            tmpUser.Documents.ForEach(doc =>
            {
                var tmpDoc = new DocumentResponse()
                {
                    Id = doc.Id,
                    AccessLevel = doc.Permissions.Find(current => current.User.Id == tmpUser.Id).Access,
                    CurrentVersion = doc.CurrentVersion,
                    Name = doc.Name,
                    RevisionResponses = new List<RevisionResponse>()
                };
                doc.Revisions.ForEach(rev =>
                tmpDoc.RevisionResponses?.Add(
                    new RevisionResponse
                    {
                        Id = doc.Id,
                        DocumentData = rev.DocumentData,
                        DocumentModifications = rev.Modifications,
                        DocumentVersion = rev.DocumentVersion
                    })
                );
                ret.Add(tmpDoc);
            });
            return Ok(ret);
        }
        [Route("GetDocumentData")]
        [HttpGet]
        public ActionResult GetDocumentData([FromQuery] int documentId)
        {
            var currentUser = HttpContext.User;
            var username = currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == username).FirstOrDefault();
            var doc = tmpUser.Documents.Find(docs => docs.Id == documentId);
            if (doc == null)
                BadRequest("no document with that id exists");

            return Ok(new DocumentDataResponse
            {
                CurrentVersion = doc.CurrentVersion,
                Data = doc.Revisions.Find(rev => rev.DocumentVersion == doc.CurrentVersion).DocumentData,
                Modifications = doc.Revisions.Find(rev => rev.DocumentVersion == doc.CurrentVersion).Modifications
            });
        }

        [Route("Create")]
        [HttpPost]
        public ActionResult Create([FromBody] CreateDocumentRequest createDocumentRequest)
        {
            var valid = new CreateDocumentValidator();
            var currentUser = HttpContext.User;
            var username = currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == username).FirstOrDefault();
            if (valid.GetValidation(createDocumentRequest.Name).HasError)
                return BadRequest(valid.Errors);
            if (_unitOfWork.Documents.Find(doc => doc.Name == createDocumentRequest.Name && tmpUser.Documents.Contains(doc)).ToList().Count > 0)
                return BadRequest("a document with that name already exsists");
            var revision = new Revision
            {
                CreatedBy = tmpUser,
                CreatedOn = DateTime.Now,
                CreatedWithTimezone = createDocumentRequest.TimeZone,
                DocumentData = "",
                DocumentOwner = tmpUser,
                DocumentVersion = "v1",
                Modifications = ""
            };

            var doc = new Document()
            {
                Name = createDocumentRequest.Name,
                CurrentVersion = "v1"
            };
            doc.Permissions.Add(new DocumentPermission() { Access = AcessLevel.FULL, Document = doc, User = tmpUser });
            doc.Revisions?.Add(revision);

            tmpUser.Documents.Add(doc);
            _unitOfWork.Documents.Add(doc);
            _unitOfWork.Users.Update(tmpUser);
            _unitOfWork.Revisions.Add(revision);
            _unitOfWork.Complete();
            return Ok();
        }

        [Route("GiveAccess")]
        [HttpPut]
        public ActionResult GiveAccess([FromQuery] string email, [FromQuery] int documentId, [FromQuery] AcessLevel accessLevel)
        {
            var currentUser = HttpContext.User;
            var username = currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == username).FirstOrDefault();
            var docs = tmpUser.Documents.Find(doc => doc.Id == documentId);
            if (docs == null) return BadRequest("a document with that id does not exist");
            var user = _unitOfWork.Users.Find(usr => usr.Detail.Email == email).FirstOrDefault();
            var acess = new DocumentPermission() { User = user, Document = docs, Access = accessLevel };
            docs.Permissions.Add(acess);
            user.Documents.Add(docs);
            _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();
            return Ok();
        }

        [Route("Save")]
        [HttpPut]
        public ActionResult Save([FromQuery] int documentId, [FromBody] SaveDocumentRequest saveDocumentRequest)
        {
            var currentUser = HttpContext.User;
            var username = currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == username).FirstOrDefault();
            var doc = tmpUser.Documents.Find(docs => docs.Id == documentId);
            if (doc == null)
                BadRequest("no document with that id exists");
            if(doc.Permissions.Find(docs=>docs.User==tmpUser).Access==0)
               return BadRequest("this user does not have write access");
            var revision = doc.Revisions.Find(rev => rev.DocumentVersion == saveDocumentRequest.RevisionVersion);
            var refRevision = doc.Revisions.Find(rev => rev.DocumentVersion == doc.CurrentVersion);
            if (revision == null)
            {
                var tmpRevision = new Revision
                {
                    CreatedBy = refRevision.CreatedBy,
                    CreatedOn = refRevision.CreatedOn,
                    CreatedWithTimezone = refRevision.CreatedWithTimezone,
                    ModifiedBy = tmpUser,
                    ModifiedOn = DateTime.Now,
                    ModifiedWithTimezone = saveDocumentRequest.TimeZone,
                    Modifications = Difference(refRevision.DocumentData, saveDocumentRequest.DocumentData, tmpUser.Username),
                    DocumentData = saveDocumentRequest.DocumentData,
                    DocumentOwner = refRevision.DocumentOwner,
                    DocumentVersion = saveDocumentRequest.RevisionVersion
                };
                doc.CurrentVersion = saveDocumentRequest.RevisionVersion;
                doc.Revisions.Add(tmpRevision);
                _unitOfWork.Documents.Update(doc);
                _unitOfWork.Complete();
                return Ok();
            }
            else
            {

                revision.ModifiedBy = tmpUser;
                revision.ModifiedOn = DateTime.Now;
                revision.ModifiedWithTimezone = saveDocumentRequest.TimeZone;
                revision.Modifications = Difference(revision.DocumentData, saveDocumentRequest.DocumentData,tmpUser.Username);
                revision.DocumentData = saveDocumentRequest.DocumentData;

                _unitOfWork.Revisions.Update(revision);
                _unitOfWork.Complete();
                return Ok();
            }
        }

        [Route("Revert")]
        [HttpPut]
        public ActionResult Revert([FromQuery] int documentId, [FromQuery] string revisionVersion)
        {
            var currentUser = HttpContext.User;
            var username = currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == username).FirstOrDefault();
            var doc = tmpUser.Documents.Find(docs => docs.Id == documentId);
            if (doc == null)
                BadRequest("no document with that id exists");
            var revision = doc.Revisions.Find(rev => rev.DocumentVersion == revisionVersion);
            if (revision == null)
                BadRequest("no revision with that version name exists");
            doc.CurrentVersion = revision.DocumentVersion;
            _unitOfWork.Documents.Update(doc);
            _unitOfWork.Complete();
            return Ok();
        }

        private string Difference(string str1, string str2,string username)
        {
            if (str1 == null)
            {
                return str2;
            }
            if (str2 == null)
            {
                return str1;
            }

            List<string> set1 = str1.Split(' ').Distinct().ToList();
            List<string> set2 = str2.Split(' ').Distinct().ToList();

            var diff = set2.Count() > set1.Count() ? set2.Except(set1).ToList() : set1.Except(set2).ToList();

            return $"<{username}>{string.Join(" ", diff)}</{username}>";
        }

    }
}

# Docs Clone Development Test
---
## Database ERD
![alt text](https://raw.githubusercontent.com/bloodchild8906/DocsClone/master/Database%20ERD.png?token=AGOURTJYUXRRV3VVUA433I3BOTMHA)
## The Setup
---
1. Clone the repository
2. install the nugetpackages
3. using the package manager console select DocsClone.EFCore as the default project and run  ```update-database```
4. next you can run the API

---

**this application uses swagger for easy api exploration**
![swagger](https://raw.githubusercontent.com/bloodchild8906/DocsClone/master/ApiImageDocs/Swagger.png?token=AGOURTOQ2QSYU2PYHAAHK5DBOTVWW)

## Account

***Auth***
**Type:Jwt**
```
-H  "Authorization: Bearer {Token Obtained from login}"
```

### Register a User
Requires Auth: false<br>
you can register a user by posting the following json to /api/account/register
```
{
  "username": "testUsername",
  "password": "string",
  "name": "nameofuser",
  "surname": "string",
  "email": "string",
  "dateOfBirth": "2021-10-17T04:01:43.736Z",
  "primaryContactNumber": "string",
  "timezone": 0
}
```
### Login
Requires Auth: false<br>
using the username and password generate a jwt token
```
{
  "username": "string",
  "password": "string"
}
```
### Update
Requires Auth: true<br>
update the logged in user to change details

```
{
  "password": "string",
  "name": "string",
  "surname": "string",
  "email": "string",
  "dateOfBirth": "2021-10-17T04:30:54.001Z",
  "primaryContactNumber": "string",
  "timezone": 0
}
```

## Document

### List
Requires Auth:true<br>

***Url: /api/Document/List***<br>

Gets a list of all documents that user has


### GetDocumentData
Requires Auth:true<br>

***/api/Document/GetDocumentData?documentId=id***<br>
gets the document data for the a document with the specified id


### Create
Requires Auth:true<br>
***Url: /api/Document/Create***<br>
creates a document for the logged in user
```
{
  "name": "string",
  "accessLevel": 0,
  "documentVersion": "string",
  "documentData": "string",
  "timeZone": 0
}
```

### GiveAccess
Requires Auth:true<br>
***Url: /api/Document/GiveAccess?email=111&documentId=1&accessLevel=0***<br>
give a user access to a document by using thier email with access level 0,1,2<br>
0: Read Only<br>
1: Write( also enables reading)<br>
2: Read/Write

### Save
Requires Auth:true<br>
***Url: /api/Document/Save?documentId=1***<br>
save data to the revision if the revision string is the same as the current but creates a new revision with the data if the revision version is different

```
{
  "revisionVersion": "string",
  "documentData": "string",
  "timeZone": 0
}
```

### Revert
Requires Auth:true<br>
***Url: /api/Document/Revert?documentId=1&revisionVersion=v1***<br>




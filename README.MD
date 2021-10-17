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
### Register a User
you can register a user by posting 
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
you can also use the swagger UI provided with the project
![alt register]()


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
![swagger](https://raw.githubusercontent.com/bloodchild8906/DocsClone/feature/Documentation_and_instructions/ApiImageDocs/register.png?token=AGOURTNCH5XPTTS6VIWG7ZDBOTPWM)

## Account

### Register a User
Requires Auth:false
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
Requires Auth:false

```
{
  "username": "string",
  "password": "string"
}
```
### Update
Requires Auth:true

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
Requires Auth:true

```

```

### GetDocumentData
Requires Auth:true

```

```

### Create
Requires Auth:true

```

```

### GiveAccess
Requires Auth:true

```

```

### Save
Requires Auth:true

```

```

### Revert
Requires Auth:true

```

```




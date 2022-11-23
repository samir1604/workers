# Workers API

- [Workers API](#workers-api)
    -[Auth](#auth)
        -[Register](#register)
            -[Register Request](#register-request)
            -[Register Response](#register-response)
        -[Login](#login)
            -[Login Request](#login-request)
            -[Login Response](login-response)

## Auth

### Register

```js
POST {{host}}/auth/register
```

#### Register Request

```json
{
    "firstName": "Samir",
    "lastName": "Reyes",
    "email": "samir1604@gmail.com",
    "password": "samir123",
}
```

#### Register Response

```js
200 OK
```

```json
{
    "id": "d98c2d9a-eb3e-4075-95ff+b920b55aa104",
    "firstName": "Samir",
    "lastName": "Reyes",
    "email": "samir1604@gmail.com",
    "token": "eybJhv...z9dcnXoY"
}
```

### Login

```js
POST {{host}}/auth/login
```

#### Login Request

```json
{
    "username": "samir",
    "password": "samir123"
}
```

#### Login Response

```js
200 OK
```

```json
{
    "firstName": "samir",
    "lastName": "samir",
    "email": "samir1604@gmail.com",
    "token": "token": "eybJhv...z9dcnXoY" 
}
```

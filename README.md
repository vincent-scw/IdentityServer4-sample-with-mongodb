# idserver4-sample-with-mongodb
This is a sample for IdentityServer4 with Mongodb working in Docker.

## Environment
1. The sample is based on .Net Core 2.0. So, you might need to update your VS2015 or VS2017 before running.
2. This sample is using Docker.

## Build & Run
1. Just simplely build & run the solution with docker (make sure docker-compose project is default).
2. With IdentityServer4 QuickStart, http://localhost:44329/account/login is for login, and http://localhost:44329/account/signup is for signup.

## Use Postman to do some tests
IdentityServer4 is working on http://localhost:44329

### Password flow
![Password](https://github.com/VincentSCW/idserver4-mongodb-sample/raw/master/pic/password_flow.png)
### Client Credentials flow
![Client Credentials](https://github.com/VincentSCW/idserver4-mongodb-sample/raw/master/pic/clientcredentials_flow.png)

# Have fun!

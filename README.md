# idserver4-mongodb-sample
This is a sample for IdentityServer4 with Mongodb.

## Environment
1. The sample is based on .Net Core 2.0. So, you might need to update your VS2015 or VS2017 before running.
1. You also need to setup a Mongodb instance in your local machine. Afterward, create "IdentityServer" database with user "admin" (use below script).
  ```Bash
  db.createUser(
    {
        user: "admin",
        pwd: "abc123!",
        roles: [{ role: "dbAdmin", db: "IdentityServer" }]
    }
  );
  ```
## Run it
1. Build and run the Sample.IdentityServer with debug setting. My configuration is using HTTPS with port 44326. You can change it.
2. With IdentityServer4 QuickStart, {url_address}/account/login is for login, and {url_address}/account/signup is for signup.

# Have fun!

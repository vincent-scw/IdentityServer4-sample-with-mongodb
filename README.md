# idserver4-mongodb-sample
This is a sample for IdentityServer4 with Mongodb.

## Environment
1. The sample is based on .Net Core 2.0. So, you might need to update your VS2015 or VS2017 before running.
1. You also need to setup a Mongodb instance in your local machine (or in Docker). Afterward, create "IdentityServer" database with user "admin" (use below script).
  ```Bash
  use IdentityServer;
  db.createUser(
    {
        user: "admin",
        pwd: "abc123!",
        roles: [{ role: "dbAdmin", db: "IdentityServer" }]
    }
  );
  ```
## Run it
1. Do some changes in your settings for project Sample.IdentityServer, as you always want HTTPS to access IdentityServer (see below). My configuration is using HTTPS with port 44329. Visual Studio will allocate one port for you, you can just copy it to startup url.

![Debug Setting](https://github.com/VincentSCW/idserver4-mongodb-sample/raw/master/pic/debug_setting.png)

2. With IdentityServer4 QuickStart, {url_address}/account/login is for login, and {url_address}/account/signup is for signup.

# Have fun!

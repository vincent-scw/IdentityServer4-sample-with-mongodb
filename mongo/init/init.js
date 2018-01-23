db.createUser(
  {
      user: "admin",
      pwd: "abc123!",
      roles: [{ role: "dbAdmin", db: "IdentityServer" }]
  }
);
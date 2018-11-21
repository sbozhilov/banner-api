# Banner API
This repository contains a simple RESTful API that store banners into MongoDB. It can be seen as a microservice, like the ones that serve advertisment to webpages. It is an exercise to keep my skill set sharp.
## What is a banner?
In this fictuanal exercise a banner is an html document.
## What is the purpose of the Banner API microservice?
Read advertisment banners on request from database. Create, Update, Delete banners from the dabase.
Simply a CRUD functionality.

# Dependancies:
1. Running mongo db on mongodb://localhost:27017 with dabase banner containg collection banner
2. .Net core 2.1

# Assumptions:
* The banner Id is unique (this design assumtion add complexity for the concusmmer to be aware of the Id as beeing unique)
* If the html sting contains a valid html tags the html is valid (alternatives to the current solution using nuget use regex(improved performence) or a www3 webservice validator(high latency))
* On Create and Update the html string is validated (this design insures that the db contains only valid html based on previous assumtion)
* Logging is done in command line, it can be replaced by logging framework
* Secrets to DB needs to be handled - it is bad to keep them in config 

# Links after you run the solution
Doc: https://localhost:44311/swagger/index.html?url=/swagger/v1/swagger.json#/BannerController
GET: https://localhost:44311/api/banner/?bannerId=<id>
DELETE: https://localhost:44311/api/banner/<id>
POST: https://localhost:44311/api/banner with Content-Type: application/json, and body like the sample JSON

# Samples of what it can do
CREATE and UDATE
POST


DELETE
?bannerId=1

Sample JSON:
{
	"Id": 1,
	"Html": '<!DOCTYPE html><html><head><title>Title of the document</title></head><body>Content of the document......</body></html>',
	"CreateTime": "2019-01-01",
	"ModifiedTime": "2019-01-02"
}


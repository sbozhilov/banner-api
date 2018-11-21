Assumptions:
* The banner Id is unique (this design assumtion add complexity for the concusmmer to be aware of the Id as beeing unique)
* If the html sting contains a valid html tags the html is valid (alternatives to the current solution using nuget use regex(improved performence) or a www3 webservice validator(high latency))
* On Create and Update the html string is validated (this design insures that the db contains only valid html based on previous assumtion)
* Logging is done in command line, it can be replaced by logging framework
* Secrets to DB needs to be handled - it is bad to keep them in config 


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


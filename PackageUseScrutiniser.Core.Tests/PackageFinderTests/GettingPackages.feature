Feature: Searching for package configs containing a package id

	Scenario: Packages exists with requested package id
		Given There are packages.config files with content
			| File name       | content                                                                                      |
			| package1.config | <?xml version="1.0" encoding="utf-8"?><packages><package id="package id"/></packages>        |
			| package2.config | <?xml version="1.0" encoding="utf-8"?><packages><package id="other package id" /></packages> |
		When getting packages with "package id"
		Then return packages containing package id
			| package file name |
			| package1.config	|

	Scenario: Packages do not exist with requested package id
		Given There are packages.config files with content
			| File name       | content                                                                                      |
			| package1.config | <?xml version="1.0" encoding="utf-8"?><packages><package id="other package id"/></packages>        |
			| package2.config | <?xml version="1.0" encoding="utf-8"?><packages><package id="other other package id" /></packages> |
		When getting packages with "package id"
		Then return packages containing package id
			| package file name |			

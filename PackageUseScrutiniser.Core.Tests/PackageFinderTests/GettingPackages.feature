Feature: Searching for package configs containing a package id

	Scenario: Packages exists with requested package id
		Given There are packages.config files with content
			| File name                     | content                                                                                                                 |
			| C:\directory1\packages.config | <?xml version="1.0" encoding="utf-8"?><packages><package id="package id 1" version="package version 1"/></packages>     |
			| C:\directory2\packages.config | <?xml version="1.0" encoding="utf-8"?><packages><package id="package id 2" version="other package version"/></packages> |
			| C:\directory3\packages.config | <?xml version="1.0" encoding="utf-8"?><packages><package id="package id 1" version="package version 2"/></packages>     |
		When getting packages with "package id 1"
		Then return packages containing package id
			| package location | package version   |
			| C:\directory1\   | package version 1 |
			| C:\directory3\   | package version 2 |

	Scenario: Packages do not exist with requested package id
		Given There are packages.config files with content
			| File name                     | content                                                                                  |
			| C:\directory1\packages.config | <?xml version="1.0" encoding="utf-8"?><packages><package id="package id 2"/></packages>  |
			| C:\directory2\packages.config | <?xml version="1.0" encoding="utf-8"?><packages><package id="package id 3" /></packages> |
		When getting packages with "package id 1"
		Then return packages containing package id
			| package location | package version   |

	Scenario: Package does exists with request package id but without version
		Given There are packages.config files with content
			| File name                    | content                                                                                |
			| C:\directory\packages.config | <?xml version="1.0" encoding="utf-8"?><packages><package id="package id" /></packages> |
		When getting packages with "package id"
		Then return packages containing package id
			| package location | package version |
			| C:\directory\    |                 |

	Scenario: Package does exists with request package id but in file not named "packages.config
		Given There are packages.config files with content
			| File name                | content                                                                                |
			| C:\directory\file.config | <?xml version="1.0" encoding="utf-8"?><packages><package id="package id" /></packages> |
		When getting packages with "package id"
		Then return packages containing package id
			| package location | package version |

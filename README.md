## Overview

"RTL.TvMazeScraper" is a set of applications that intended to ingest data from TvMaze and provide via REST API.
"RTL.TvMazeScraper" includes four projects at this moment:
1. RTL.TvMazeScraper.ConsoleApp is a simple console application that intentded to scrape the data from TvMaze API
2. RTL.TvMazeScraper.Core is a libary that contains domain entities and interfaces
3. RTL.TvMazeScraper.Sql is a library that contains implementation of domain interfaces that employs SqlLite databse and Entity Framework Core techologies
4. RTL.TvMazeScraper.WebApi is a simple web application that that provide scraped data in JSON format

## Open problems

Current solution contains several open problems that could be mitigated in future:
1. At this moment solution does not contains any kind of tests(integration, load, unit)
2. For this specific business requirements problably differnt storage engine can be used, for example MongoDB
3. It makes sense to add caching as soon as data is going to be relative static
4. Logging and error handling strategies could be revised
5. Configuration of database location and page size should be moved to config file

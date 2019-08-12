To Run the unit tests or postman tests, please run BizCover.Api.CachedRepo first in your IIS and change the URL to your local host.
To Debug BizCover.Api.Cars, please run BizCover.Api.CachedRepo first in your IIS and change the URL to your local host.
To Run the postman tests, please import BizCover.postman_collection.json and change the URL to your local host.

For some reason, BizCover.Api.Cars cannot inject the HTTPCLIENT, more investigation may required.
For some reason, DLL provided in the test cannot working, and mocked version included as a mutiple-threading solution
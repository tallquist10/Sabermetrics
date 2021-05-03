module Sabermetrics.Exceptions
type Exception =
| PlayerNotFoundException
| PlayerAlreadyInDatabase
| SqliteException of string
| FailedDomainTranslationException of string
| FailedWebRequestException of string
| FailedWebResponseParseException of string

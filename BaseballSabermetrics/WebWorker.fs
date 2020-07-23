namespace Sabermetrics

module WebWorker =
    open System
    open System.Net
    open System.Text.RegularExpressions
    open System.IO
    open FSharp.Data

    type ResponseWrapper =
    | Html of WebResponse
    | Other
    let getSite (url:string) =
        HtmlDocument.Load(url)


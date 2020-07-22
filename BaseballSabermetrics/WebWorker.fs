namespace Sabermetrics

module WebWorker =
    open System
    open System.Net
    open System.Text.RegularExpressions
    open System.IO

    type ResponseWrapper =
    | Html of WebResponse
    | Other
    let getSite url =
        let request = WebRequest.Create (new Uri(url)) : WebRequest
        request.GetResponse()

    let wrapResonse (response: WebResponse) =
        let isHtml = Regex("html").IsMatch(response.ContentType)
        match isHtml with
        | true -> Html response
        | false -> Other

    let handleResponse (response:ResponseWrapper) =
        match response with
        | Html r ->
            use stream = r.GetResponseStream()
            use reader = new StreamReader(stream)
            let html = reader.ReadToEnd()
            Some html
        | _ -> None


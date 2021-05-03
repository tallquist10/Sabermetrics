namespace Sabermetrics.Dependencies
module ExcelExport =
    open OfficeOpenXml
    open System.IO

    let writeToCell row column value (sheet: OfficeOpenXml.ExcelWorksheet) = 
        try
            sheet.Cells.[row,column].Value <- value
            sheet
        with
        | ex -> sheet
    let createExcel name =
        let file = new FileInfo(name)
        match File.Exists(name) with
        | true -> ()
        | false -> file.Directory.Create()
        
        let excelPackage = new ExcelPackage(file)
        match (excelPackage.Workbook.Worksheets.Item "Hitting stats") with
        | null -> excelPackage.Workbook.Worksheets.Add "Hitting stats" |> ignore
        | _ -> ()
        excelPackage.Save()
        excelPackage

    let getWorksheet (package: OfficeOpenXml.ExcelPackage) =
        package.Workbook.Worksheets.Item 0

    let addStatsToSheet (sheet: OfficeOpenXml.ExcelWorksheet) row (stats: Map<string,'a>) =
        sheet
        |> writeToCell row 1 stats.["Name"]
        |> writeToCell row 2 stats.["G"]
        |> writeToCell row 3 stats.["BA"]
        |> writeToCell row 4 stats.["H"]
        |> writeToCell row 5 stats.["Doubles"]
        |> writeToCell row 6 stats.["Triples"]
        |> writeToCell row 7 stats.["HR"]
        |> writeToCell row 8 stats.["RBI"]
        |> writeToCell row 9 stats.["R"]
        |> writeToCell row 10 stats.["SB"]
        |> writeToCell row 11 stats.["CS"]
        |> writeToCell row 12 stats.["BB"]
        |> writeToCell row 13 stats.["SO"]
        |> writeToCell row 14 stats.["PA"]
        |> writeToCell row 15 stats.["AB"]
        |> writeToCell row 16 stats.["OBP"]
        |> writeToCell row 17 stats.["SLG"]
        |> writeToCell row 18 stats.["OPS"]
        |> writeToCell row 19 stats.["OPSPlus"]
        |> writeToCell row 20 stats.["TB"]
        |> writeToCell row 21 stats.["GDP"]
        |> writeToCell row 22 stats.["HBP"]
        |> writeToCell row 23 stats.["SH"]
        |> writeToCell row 24 stats.["SF"]
        |> writeToCell row 25 stats.["IBB"]

    let addHeaderRows (sheet: OfficeOpenXml.ExcelWorksheet) =
        let headers = [|"Name"; "G"; "BA"; "H"; "2B"; "3B"; "HR"; "RBI"; "R"; "SB"; "CS"; "BB"; "SO"; "PA"; "AB"; "OBP"; "SLG"; "OPS"; "OPS+"; "TB"; "GDP"; "HBP"; "SH"; "SF"; "IBB"|]
        let mutable statSheet = sheet
        for i in 0..headers.Length-1 do
            statSheet <- writeToCell 1 (i+1) headers.[i] statSheet
        
        statSheet
         
    //let insertPlayersToCells (players: array<Map<string,'a>>) =
    //    let excel = createExcel (Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()) + "sabermetrics/baseball/stats.xlsx")
    //    let mutable statSheet = getWorksheet excel |> addHeaderRows
    //    for player in players do
    //        Map.iteri (fun idx (_,v) -> (statSheet <- addStatsToSheet statSheet (idx+2) v; excel.Save())) player
    //    printfn "Stat sheet created at %s" (Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()) + "sabermetrics/baseball/stats.xlsx")
        


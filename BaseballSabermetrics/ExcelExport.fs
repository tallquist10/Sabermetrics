namespace Sabermetrics
module ExcelExport =
    open OfficeOpenXml
    open System.IO
    open BaseballDataCollector
    open Stats



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

    let addStatsToSheet (sheet: OfficeOpenXml.ExcelWorksheet) row (player: Hitter) =
        sheet
        |> writeToCell row 1 player.Name
        |> writeToCell row 2 (player.G |> ExtractValue)
        |> writeToCell row 3 (player.BA |> ExtractValue)
        |> writeToCell row 4 (player.H |> ExtractValue)
        |> writeToCell row 5 (player.Doubles |> ExtractValue)
        |> writeToCell row 6 (player.Triples |> ExtractValue)
        |> writeToCell row 7 (player.HR |> ExtractValue)
        |> writeToCell row 8 (player.RBI |> ExtractValue)
        |> writeToCell row 9 (player.R |> ExtractValue)
        |> writeToCell row 10 (player.SB |> ExtractValue)
        |> writeToCell row 11 (player.CS |> ExtractValue)
        |> writeToCell row 12 (player.BB |> ExtractValue)
        |> writeToCell row 13 (player.SO |> ExtractValue)
        |> writeToCell row 14 (player.PA |> ExtractValue)
        |> writeToCell row 15 (player.AB |> ExtractValue)
        |> writeToCell row 16 (player.OBP |> ExtractValue)
        |> writeToCell row 17 (player.SLG |> ExtractValue)
        |> writeToCell row 18 (player.OPS |> ExtractValue)
        |> writeToCell row 19 (player.OPSPlus |> ExtractValue)
        |> writeToCell row 20 (player.TB |> ExtractValue)
        |> writeToCell row 21 (player.GDP |> ExtractValue)
        |> writeToCell row 22 (player.HBP |> ExtractValue)
        |> writeToCell row 23 (player.SH |> ExtractValue)
        |> writeToCell row 24 (player.SF |> ExtractValue)
        |> writeToCell row 25 (player.IBB |> ExtractValue)
         
    let insertPlayersToCells (players: Hitter []) =
        let excel = createExcel (Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()) + "sabermetrics/baseball/stats.xlsx")
        let mutable statSheet = getWorksheet excel
        for i in 1..players.Length do
            statSheet <- addStatsToSheet statSheet (i+2) players.[i] 
            excel.Save()
        
        


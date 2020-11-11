namespace Components

open Feliz
open Feliz.MaterialUI
open Feliz.Recharts
open Shared




module SalesGraph =


  let chart (data : SalesGraphPoint list) =
    let maxofList =
      data
      |> List.map (fun x -> x.Boxes)
      |> List.max
      |> int
      |> (+) 25

    let ymin = domain.constant 0, domain.constant maxofList
    Mui.container [
    Recharts.composedChart [
        composedChart.width 600
        composedChart.height 250
        composedChart.data data
        composedChart.children [
            Recharts.cartesianGrid [ cartesianGrid.strokeDasharray(25, 1) ]
            Recharts.xAxis [ xAxis.dataKey (fun point -> point.Period) ]
            Recharts.yAxis [ yAxis.number; yAxis.domain ( ymin ) ]
            Recharts.tooltip [ ]
//            Recharts.legend []
            Recharts.bar [
                bar.dataKey (fun point -> (point.Boxes).ToString("0.00"))
                bar.name "Boxes"
                bar.fill "#008800"
                bar.strokeWidth 150
            ]
            Recharts.line [
                line.dataKey (fun point -> point.QtrTrend.ToString("0.00"))
                line.fill "#333333"
                line.stroke "#ff0000"
                line.name "Quarter average"
            ]
        ]
      ]
    ]
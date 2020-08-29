namespace Components

open Feliz
open Feliz.Recharts
open Shared

module SalesGraph =
  let chart (data : SalesGraphPoint list) =
    Recharts.composedChart [
        composedChart.width 650
        composedChart.height 250
        composedChart.data data
        composedChart.children [
            Recharts.cartesianGrid [ cartesianGrid.strokeDasharray(3, 3) ]
            Recharts.xAxis [ xAxis.dataKey (fun point -> point.Period) ]
            Recharts.yAxis [ ]
            Recharts.tooltip [ ]
//            Recharts.legend []
            Recharts.bar [
                bar.dataKey (fun point -> point.Boxes.ToString("0.00"))
                bar.name "Boxes"
                bar.fill "#008800"
            ]
            Recharts.line [
                line.dataKey (fun point -> point.QtrTrend.ToString("0.00"))
                line.fill "#333333"
                line.stroke "#ff0000"
                line.name "Quarter average"
            ]
        ]
      ]
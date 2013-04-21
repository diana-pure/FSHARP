#light
open Microsoft.FSharp.Math
open System
open System.Drawing
open System.Windows.Forms

//crete scene and fill background color
let plot = new Bitmap(600, 400)
for j = 0 to(plot.Height - 1) do
    for i = 0 to(plot.Width - 1) do
        plot.SetPixel(i,j, Color.White)

//converts y coordinates to the image coordinates
let toSceneCoordY y = 
    plot.Height / 2 - int (100.0 * y)

//converts x coordinates to the plot coordinates  
let toPlotCoordX x = 
      double(x - plot.Width / 2) / 100.0

//converts y coordinates to the plot coordinates  
let toPlotCoordY y = 
      double(plot.Height / 2 - y) / 100.0

//cosinus: using plot's coordinates (not image's)
let cosinus x = 
     cos(toPlotCoordX(x))

//main function that defines heart-plot
let heartPlot x = 
    toSceneCoordY(sqrt(cosinus(x)) * cosinus(200 * x) + (sqrt(abs(toPlotCoordX(x))) - 0.7) * (4.0 - toPlotCoordX(x) ** 2.0) ** 0.01)

//sat extra 8 pixels around (i, j)
let setBigPixel x = 
    for i = -3 to 3 do
        for j = -3 to 3 do
            plot.SetPixel(x + j , heartPlot(x) + i, Color.Red)

//construct output image
for x = 0 to plot.Width - 1 do
        for y = 0 to plot.Height - 1 do
           if ((heartPlot(x) > 0) && (heartPlot(x) < plot.Height)) then
                setBigPixel(x)

//to display results
let window = new Form()
window.Paint.Add(fun e -> e.Graphics.DrawImage(plot, 0, 0))
window.Show()
window

do Application.Run()

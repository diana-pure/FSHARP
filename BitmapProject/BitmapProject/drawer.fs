#light
open Microsoft.FSharp.Math
open System
open System.Drawing
open System.Windows.Forms

//crete scene and fill background color
let plot = new Bitmap(600, 600)
for j = 0 to(plot.Height - 1) do
    for i = 0 to(plot.Width - 1) do
        plot.SetPixel(i,j, Color.White)

//converts y coordinates to the image coordinates
let toSceneCoordX x = 
    plot.Width / 2 + int (100.0 * x)

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

//top half-circle
let halfCircle1 x = 
    toSceneCoordY(sqrt(9.0 - toPlotCoordX(x) ** 2.0))

//bottom half-circle
let halfCircle2 x = 
    toSceneCoordY(-sqrt(9.0 - toPlotCoordX(x) ** 2.0))

//sat extra 8 pixels around (i, j)
let setBigPixel (x, y, color) = 
    for i = -3 to 3 do
        for j = -3 to 3 do
            if (((x + j) > 0) && ((x + j) < plot.Width)) then
                if (((y + i) > 0) && ((y + i) < plot.Height)) then
                    plot.SetPixel(x + j , y + i, color)

//construct output image
for x = toSceneCoordX(-3.0) to toSceneCoordX(3.0) do
        for y = toSceneCoordY(2.0) to toSceneCoordY(-2.0) do
                setBigPixel(x, heartPlot(x), Color.Red)
                setBigPixel(x, halfCircle1(x), Color.LimeGreen)
                setBigPixel(x, halfCircle2(x), Color.LimeGreen)

//to display results
let window = new Form()
window.Width <- 620
window.Height <- 640
window.Paint.Add(fun e -> e.Graphics.DrawImage(plot, 0, 0))
window.Show()
window

do Application.Run()

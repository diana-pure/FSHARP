#light
open Microsoft.FSharp.Math
open System
open System.Drawing
open System.Windows.Forms
(*
//crete scene and fill background color
let plot = new Bitmap(400, 400)
for i = 0 to(plot.Height - 1) do
    for j = 0 to(plot.Width - 1) do
        plot.SetPixel(i,j, Color.White)

plot.SetPixel(200, 200, Color.Black)

//to display results
let window = new Form()
window.Paint.Add(fun e -> e.Graphics.DrawImage(plot, 0, 0))
window.Show()
window

do Application.Run()
*)

let cMax = complex 1.0 1.0
let cMin = complex -1.0 -1.0

let rec isInMandelbrotSet (z, c, iter, count) =
    if (cMin < z) && (z < cMax) && (count < iter) then
        isInMandelbrotSet ( ((z * z) + c), c, iter, (count + 1) )
    else count

let scalingFactor s = s * 3.0 / 200.0
let offsetX = -3.0
let offsetY = -3.0

let mapPlane (x, y, s, mx, my) =
    let fx = ((float x) * scalingFactor s) + mx
    let fy = ((float y) * scalingFactor s) + my
    complex fx fy

let colorize c =
    let r = (4 * c) % 255
    let g = (6 * c) % 255
    let b = (8 * c) % 255
    Color.FromArgb(r,g,b)

let heartPlot x1 = 
        //((cos(x) ^ 0.5) * cos(200.0 * x) + ((abs(x)) ^ 0.5 - 0.7) * ((4 - x * x) ^ 0.01)
     //int (sin(double(x1 / 10)) * 20.0)
     int (200.0 - 30.0 * cos(double(x1 - 200) * 3.14 / 60.0) + 0.5)

let createImage (s, mx, my) =
    let image = new Bitmap(400, 400)
    for i = 0 to(image.Height - 1) do
        for j = 0 to(image.Width - 1) do
            image.SetPixel(i,j, Color.White)
    for x = 0 to image.Width - 1 do
        for y = 0 to image.Height - 1 do
           if ((heartPlot(x) > 0) && (heartPlot(x) < image.Height)) then
                image.SetPixel(x , heartPlot(x), Color.Red)            
    let temp = new Form() in
    temp.Paint.Add(fun e -> e.Graphics.DrawImage(image, 0, 0))
    temp

do Application.Run(createImage (1.0, -1.5, -1.5))
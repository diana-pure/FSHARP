#light
open Microsoft.FSharp.Math
open System
open System.Drawing
open System.Windows.Forms

let plot = new Bitmap(400, 400)
for i = 0 to(plot.Height - 1) do
    for j = 0 to(plot.Width - 1) do
        plot.SetPixel(i,j, Color.White)
plot.SetPixel(200, 200, Color.Black)
let window = new Form()
window.Paint.Add(fun e -> e.Graphics.DrawImage(plot, 0, 0))
window.Show()
window

do Application.Run()
## Installs
All VSCode exntensions
======
1.	https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp
2.	https://marketplace.visualstudio.com/items?itemName=Ionide.Ionide-fsharp
3.	https://marketplace.visualstudio.com/items?itemName=ms-toolsai.jupyter
4.	https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.dotnet-interactive-vscode
5.	https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-pack

.NET SDK
=======
1.	https://dotnet.microsoft.com/en-us/download/dotnet/6.0
2.	https://dotnet.microsoft.com/en-us/download/dotnet/7.0

## Commands
`dotnet new sln --name FilesToPDF`
`dotnet new webapi -o FilesToPDF.Api`
`dotnet sln add FilesToPDF.Api`

`dotnet add C:\code\FilesToPDF\FilesToPDF.Api package iTextSharp`
`dotnet add C:\code\FilesToPDF\FilesToPDF.Api package Microsoft.Office.Interop.Word`
`dotnet add C:\code\FilesToPDF\FilesToPDF.Api package Microsoft.Office.Interop.Excel`
`dotnet add C:\code\FilesToPDF\FilesToPDF.Api package Microsoft.Office.Interop.PowerPoint`
`dotnet add package MSOffice.Object.Library`
`dotnet add package PdfSharpCore`
`dotnet add package Magick.NET-Q16-AnyCPU`
`dotnet add package Serilog`
`dotnet add package Serilog.Extensions.Logging`
`dotnet add package Serilog.Extensions.Logging.File`

`dotnet sln list`
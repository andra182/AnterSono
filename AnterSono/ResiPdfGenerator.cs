using System;
using System.Drawing;
using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ZXing;
using ZXing.Common;

namespace AnterSono
{
    public class Paket
    {
        public string Resi { get; set; }
        public int IdPengirim { get; set; }
        public string NamaPengirim { get; set; }
        public string NoHpPengirim { get; set; }
        public string NamaPenerima { get; set; }
        public string NoHpPenerima { get; set; }
        public string NamaBarang { get; set; }
        public decimal Berat { get; set; }
        public string AlamatAsal { get; set; }
        public string AlamatTujuan { get; set; }
        public int Jarak { get; set; }
        public decimal HargaTotal { get; set; }
        public string MetodePembayaran { get; set; }
        public string TipePengiriman { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public static class PdfResiGenerator
    {
        public static void Generate(Paket paket, byte[] logoBytes, string outputPath)
        {
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions { Height = 60, Width = 300, Margin = 1 }
            };
            var pixelData = writer.Write(paket.Resi);
            byte[] barcodeBytes;
            using (var bmp = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                var data = bmp.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly, bmp.PixelFormat);
                try { System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, data.Scan0, pixelData.Pixels.Length); }
                finally { bmp.UnlockBits(data); }

                using (var ms = new MemoryStream())
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    barcodeBytes = ms.ToArray();
                }
            }

            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A6);
                    page.Margin(10);

                    page.Header().Row(row =>
                    {
                        row.ConstantColumn(100)
                           .Height(25)
                           .Image(logoBytes);
                        row.ConstantColumn(10);
                        
                    });

                    page.Content().Column(col =>
                    {
                        col.Item().LineHorizontal((float)0.5, Unit.Point);

                        col.Item().AlignCenter().Image(barcodeBytes);

                        col.Item().AlignCenter().Text($"RESI: {paket.Resi}")
                            .SemiBold().FontSize(10);

                        col.Spacing(2);

                        col.Item().LineHorizontal((float) 0.5, Unit.Point);

                        col.Spacing(2);

                        var label = paket.MetodePembayaran == "COD" ? "COD" : "Non‑COD";
                        col.Item().AlignCenter().Text($"{label} : Rp{paket.HargaTotal:N0}")
                           .Bold().FontSize(20);

                        col.Item().LineHorizontal((float)0.5, Unit.Point);

                        col.Spacing(5);

                        col.Item().Row(r =>
                        {
                            r.RelativeColumn().Text($"Qty: 1 | {paket.NamaBarang}");
                            r.RelativeColumn().AlignRight().Text($"Berat: {paket.Berat * 1000:N0} gr");
                        });

                        col.Spacing(2);

                        col.Item().LineHorizontal((float)0.5, Unit.Point);

                        col.Item().Row(r =>
                        {
                            r.RelativeColumn().Column(left =>
                            {
                                left.Item().Text("Penerima:").Bold();
                                left.Item().Text(paket.NamaPenerima);
                                left.Spacing(2);
                                left.Item().Text(paket.AlamatTujuan);
                                left.Spacing(2);
                                left.Item().Text($"No HP: {paket.NoHpPenerima}");
                            });

                            r.ConstantColumn(10);

                            r.RelativeColumn().Column(right =>
                            {
                                right.Item().Text("Pengirim:").Bold();
                                right.Item().Text(paket.NamaPengirim);
                                right.Spacing(2);
                                right.Item().Text(paket.AlamatAsal);
                                right.Spacing(2);
                                right.Item().Text($"No HP: {paket.NoHpPengirim}");
                            });
                        });

                        col.Item().LineHorizontal((float)0.5, Unit.Point);

                        col.Item().Row(r =>
                        {
                            r.RelativeColumn().Text($"Layanan: {paket.TipePengiriman}");
                            r.RelativeColumn().AlignRight()
                                .Text(paket.CreatedAt.ToString("dd MMM yyyy HH:mm"));
                        });
                    });

                    page.Footer().AlignCenter()
                        .Text("*Mohon simpan bukti ini untuk tracking*")
                        .FontSize(8);
                });
            });

            doc.GeneratePdf(outputPath);
        }
    }

    // Contoh penggunaan:
    // var paket = new Paket { Resi="REG562964", IdPengirim=1, NamaPenerima="Sugeng", ... };
    // var logo = File.ReadAllBytes("logo.png");
    // PdfResiGenerator.Generate(paket, logo, "resi.pdf");
}

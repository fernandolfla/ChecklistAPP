using System;
using System.Collections.Generic;
using System.Text;

namespace ChecklistAPP.Models
{
    public class Check_Foto
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public int CheckId { get; set; }
        public Check Check { get; set; }
        public byte[] Foto { get; set; }


        //public class MyModel                //Método para pegar a foto do celular tipo do arquivo
        //{
        //    public IFormFile Arquivo { get; set; }
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Upload(MyModel model)                         // Exemplo de método para receber e converter a foto
        //{
        //    if (ModelState.IsValid)
        //    {
        //        MyObject obj = new MyObject();

        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await model.Arquivo.CopyToAsync(memoryStream);
        //            //Recebo o stream, passo pra bytearray
        //            byte[] barray = memoryStream.ToArray();
        //            //passo o array para o objeto
        //            obj.Foto = barray;

        //        }

        //        await _context.MyObjects.AddAsync(obj);
        //        _context.SaveChanges();
        //    }

        //    return View("Index");
        //}

    }
}

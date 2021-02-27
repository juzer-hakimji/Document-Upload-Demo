using DocumentDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        [HttpPost("DocumentUpload")]
        public async Task<IActionResult> DocumentUpload(DocumentListVM DocumentInputModel)
        {
            try
            {
                DocumentDemoContext db = new DocumentDemoContext();
                List<Document> DocList = GetDocList(DocumentInputModel.DocumentList);
                await db.Documents.AddRangeAsync(DocList);
                int NoOfRowsInserted = await db.SaveChangesAsync();
                if (NoOfRowsInserted > 0)
                {
                    return Ok(new { message = "Documents Saved Successfully", Data = DocList });
                }
                else
                {
                    return Ok(new { message = "Something went wrong. Please try again." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }


        private List<Document> GetDocList(DocumentVM[] lstDocVM)
        {
            //converting document array received to database document table list
            List<Document> DBDocList = new List<Document>();
            foreach (var Doc in lstDocVM)
            {
                // dividing file content from file type
                Doc.DocumentContent = Doc.DocumentContent.Substring(Doc.DocumentContent.IndexOf(",") + 1);
                DBDocList.Add(new Document
                {
                    DocumentName = Doc.DocumentName,
                    DocumentContent = Convert.FromBase64String(Doc.DocumentContent),
                    ContentType = Doc.ContentType
                });
            }
            return DBDocList;
        }

        [HttpGet("DownloadDocument/{DocumentId}")]
        public IActionResult DownloadDoument(long DocumentId)
        {
            try
            {
                DocumentDemoContext db = new DocumentDemoContext();
                Document doc = db.Documents.FirstOrDefault(x => x.DocumentId == DocumentId);
                return File(doc.DocumentContent, doc.ContentType, doc.DocumentName);
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }

        }
    }
}

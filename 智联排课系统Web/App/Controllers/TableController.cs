using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace App.Controllers
{
    public class TableController : ApiController
    {
        public BLL.YiCiPaiKeDeKeBiaoXinXiBiaoBLL bLL = new BLL.YiCiPaiKeDeKeBiaoXinXiBiaoBLL();

        //查询课表

        [HttpGet]
        public IHttpActionResult ShowTable(string Name, int? page, int? limit)
        {
            int PageIndex = page ?? 1;
            int PageSize = limit ?? 10;
            ////int PageIndex = page.PageIndex == 0 ? 1 : page.PageIndex;
            ////int PageSize = page.PageSize == 0 ? 10 : page.PageSize;
            //DateTime datetime = page.Time == null ? new DateTime(0001, 1, 1) : page.Time;
            //string Name = "";
            DateTime datetime2 = new DateTime(0001, 1, 1);
            List<YiCiPaiKeDeKeBiaoXinXiBiao> List = bLL.List(PageIndex, PageSize, Name, datetime2, out int Total);
            var Json = new
            {
                code = 0,
                msg = "成功",
                count = Total,
                data = List
            };
            return Ok(Json);
        }

        public class Page
        {
            public int PageIndex { get; set; }

            public int PageSize { get; set; }

            public string Name { get; set; }

            public DateTime Time { get; set; }
        }
    }


}

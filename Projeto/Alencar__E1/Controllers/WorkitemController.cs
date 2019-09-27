using System;
using System.Linq;
using System.Web.Http;
using Alencar__E1.Models;

namespace Alencar__E1.Controllers
{
    [RoutePrefix("API/Workitem")]
    public class WorkitemController : ApiController
    {
        WorkitemEntities objEntity = new WorkitemEntities();

        [HttpGet]
        [Route("AllWorkitems")]
        public IQueryable<TB_Workitem> GetWorkitem()
        {
            try
            {
                return objEntity.TB_Workitem;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetWorkitemById/{workitemId}")]
        public IHttpActionResult GetWorkitemId(string workitemId)
        {
            TB_Workitem objWork = new TB_Workitem();
            int ID = Convert.ToInt32(workitemId);
            try
            {
                objWork = objEntity.TB_Workitem.Find(ID);
                if(objWork == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(objWork);
        }

        [HttpPost]
        [Route("InsertWorkitems")]
        public IHttpActionResult PostWorkitem(TB_Workitem data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                objEntity.TB_Workitem.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(data);

        }

        [HttpPut]
        [Route("UpdateWorkitems")]
        public IHttpActionResult PutWorkitemMT(TB_Workitem workitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TB_Workitem objWork = new TB_Workitem();
                objWork = objEntity.TB_Workitem.Find(workitem.ID);
                if(objWork != null)
                {
                    objWork.ID = workitem.ID;
                    objWork.Tipo = workitem.Tipo;
                    objWork.Titulo = workitem.Titulo;
                    objWork.DataCriacao = workitem.DataCriacao;
                }

                int i = this.objEntity.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(workitem);
        }

        [HttpDelete]
        [Route("DeleteWorkitems")]
        public IHttpActionResult DeleteWorkitemDL(int id)
        {
            TB_Workitem workitem = objEntity.TB_Workitem.Find(id);
            if(workitem == null)
            {
                return NotFound();
            }

            objEntity.TB_Workitem.Remove(workitem);
            objEntity.SaveChanges();

            return Ok(workitem);
        }

    }
}

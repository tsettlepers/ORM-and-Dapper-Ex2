using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_and_Dapper_Ex2
{
    public class ProductTools : OurDBTools
    {
        public override OurDBObjects Get(int id)
        {
            try
            {
                //The Query() method is used here because Get() doesn't support two columns being marked [Key].
                var res = Conn.Query<Product>($"select * from Products where ProductID={id};");
                foreach (var item in res)
                {
                    item.IsDirty = false;
                    item.EntityType = "PRODUCT";
                    item.RetrievedAt = DateTime.Now;
                    return item;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Product.Get() failed. Message: " + ex.Message);
            }
        }

        public override IEnumerable<OurDBObjects> GetAll()
        {
            try
            {
                var res = Conn.Query<Product>("select * from Products where Deleted is NULL;");
                foreach (var item in res)
                {
                    item.IsDirty = false;
                    item.EntityType = "PRODUCT";
                    item.RetrievedAt = DateTime.Now;
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Product.GetAll() failed. Message: " + ex.Message);
            }
        }

        public override bool Create(OurDBObjects newRec)
        {
            try
            {
                Product p = (Product)newRec;
                var rid = Conn.Insert<Product>(p);
                if (rid > 0)
                {
                    p.ProductID = rid;
                    p.IsDirty = false;
                    p.EntityType = "PRODUCT";
                    p.RetrievedAt = DateTime.Now;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Product.Create() failed. Message: " + ex.Message);
            }
        }

        public override bool Update(OurDBObjects thisRec)
        {
            Product p = (Product)thisRec;
            var res = Conn.Update(p);
            if (res == true)
            {
                p.IsDirty = false;
                p.RetrievedAt = DateTime.Now;
                return true;
            }
            else
                return false;
        }

        public override bool Delete(int id)
        {
            try
            {
                var recs = Conn.Execute($"update Products set Deleted=now() where ProductID={id}");
                return recs==1 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception("Product.Delete() failed. Message: " + ex.Message);
            }
        }
    }
}

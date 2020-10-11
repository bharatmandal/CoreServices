using CoreServices.Models;
using CoreServices.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CoreServices.Repository
{
    public class PostRepository : IPostRepository
    {
        BlogDBContext db;
        public PostRepository(BlogDBContext _db)
        {
            db = _db;

        }

        public async Task<List<Category>> GetCategory()
        {
            if (db!=null) {
                //below for without parameter
                //return await db.Category.FromSqlRaw("Sp_Categories").ToListAsync();
                //Below for SP with paramter
                //return await db.Category.FromSqlInterpolated($"Sp_Categories_byID {1}").ToListAsync();

                //With one named parameter
                //int nCatID = 1;
                //var catParam = new SqlParameter("@Categoryid", nCatID);
                //return await db.Category.FromSqlRaw("Sp_Categories_byID @CategoryID", catParam).ToListAsync();

                //With multiple named input parameter
                //int nCatID = 1;
                //string strcatName = "CSHARP";
                //var catParamID = new SqlParameter("@Categoryid", nCatID);
                //var catParamName = new SqlParameter("@CategoryName", strcatName);

                //return await db.Category.FromSqlRaw("Sp_Categories_ByID_Name @CategoryID,@CategoryName", catParamID, catParamName).ToListAsync();

                //using as interpolation
                int nCatID = 1;
                string strcatName = "CSHARP";
                return await db.Category.FromSqlRaw($"Sp_Categories_ByID_Name {nCatID},{strcatName}").ToListAsync();

                //below no SP
                //return await db.Category.ToListAsync();

            }
            return null;
            
        }


        public async Task<int> AddPost(Post post)
        {
            if (db !=null)

            {
                try
                {
                    await db.Post.AddAsync(post);
                    await db.SaveChangesAsync();
                    return post.PostId ;
                }
                
                catch
                {

                }
                
            }
            return 0;
            
        }

        public async  Task<int> AddCategoryBySP(Category category )
        {
            if (db != null)

            {
                try
                {
                    int nRec=1;
                    string sCategoryName = category.Name;
                    string sCategorySlug = category.Slug ;
                    
                    var CategoryName = new SqlParameter("@Name", sCategoryName);
                    var CategorySlug = new SqlParameter("@SLUG", sCategorySlug);

                    nRec= await db.Database.ExecuteSqlRawAsync ("SP_AddCategory @Name,@SLUG", CategoryName, CategorySlug);
                    return nRec;
                                      
                 }

                catch (Exception ex)
                {
                    string str;
                    str = ex.ToString();
                    str = "1";
                }


            }
            return 0;

        }

        

        public Task<int> DeletePost(int? postid)
        {
            throw new NotImplementedException();
        }

        
        public async Task<List<PostViewModel>> GetPost()
        {
            if (db != null)
            {
                return await (from p in db.Post
                              from c in db.Category
                              where p.CategoryId == c.Id
                              select new PostViewModel
                              {
                                  PostId=p.PostId,
                                  Title=p.Title,
                                  CategoryId =p.CategoryId ,
                                  CategoryName=c.Name,
                                  CreatedDate=p.CreatedDate
                              }).ToListAsync();
            }
            return null;
        }

        public async Task<List<Sp_GetAllPost>> GetPostWithSP()
        {
            if (db != null)
            {
                return await db.Sp_GetAllPost.FromSqlRaw("Sp_GetAllPost").ToListAsync();
                
                //return await db.Category.FromSqlRaw("Sp_GetAllPost").ToListAsync();
            }
            return null;
        }

        public Task<List<PostViewModel>> GetPost(int? postid)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePost(Post post)
        {
            if (db != null)
            {
                //Delete that post
                db.Post.Update(post);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task<int> AddTestTable(TestTable testTable)
        {
            if (db != null)
            {

                try
                {
                    await db.TestTable.AddAsync(testTable);
                    await db.SaveChangesAsync();
                    return testTable.Id;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return 0;
        }

        Task IPostRepository.UpdateTestTableBySP()
        {
            try
            {
                var recid= db.Database.ExecuteSqlRawAsync("InsertTestTable");
                return recid;
                
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<List<string>>  RetrieveTestTableBySP()
        {
            List<string> mylist= new List<string>();
            mylist.Add("m1");
            mylist.Add("m2");
            mylist.Add("m3");
            mylist.Add("m4");

            return  mylist;
            //List<ItemList>= await db.Database.ExecuteSqlRawAsync("RetrieveTestTable");
            //return ItemList;
        }
    }
}

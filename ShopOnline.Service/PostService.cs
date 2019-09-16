using ShopOnline.Data.Infrastracture;
using ShopOnline.Data.Repositories;
using ShopOnline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Service
{
    public interface IPostService
    {
        void Add(Post post);
        void Update(Post post);
        void Delete(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAllPaging(int page,int PageSize, out int totalRow);
        IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow);
        Post GetById(int id);
        IEnumerable<Post> GetAllTagPaging(string tag,int page, int PageSize, out int totalRow);
        void saveChanges();
    }
    public class PostService : IPostService
    {
        IPostRepository _postRepository;
        IUnitOfWork _unitofWork;
        public PostService(IPostRepository postRepository , IUnitOfWork unitofWork)
        {
            this._postRepository = postRepository;
            this._unitofWork = unitofWork;
        }
        public void Add(Post post)
        {
            _postRepository.Add(post);
        }

        public void Delete(int id)
        {
            _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll(new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status && x.PostCategoryID == categoryId, out totalRow, page, pageSize, new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllPaging(int page, int PageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status, out totalRow, page, PageSize);
        }

        public IEnumerable<Post> GetAllTagPaging(string tag,int page, int PageSize, out int totalRow)
        {
            //TODO select all post by tag
            return _postRepository.GetAllByTag(tag,page,PageSize, out totalRow);
        }

        public Post GetById(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public void saveChanges()
        {
            _unitofWork.Commit();
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
        }
    }
}

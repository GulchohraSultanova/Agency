using Bussiness.Abstracts;
using Bussiness.Exceptions;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Concreters
{
    public class PortfolioService : IPortfolioService
    {
        IPortfolioRepository _portfolioRepository;
       IWebHostEnvironment _webHostEnvironment;

        public PortfolioService(IPortfolioRepository portfolioRepository, IWebHostEnvironment webHostEnvironment)
        {
            _portfolioRepository = portfolioRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Create(Portfolio portfolio)
        {
           if(portfolio == null)
            {
                throw new NotFoundException("Bele bir obyekt tapilmadi!");
            }
           if(portfolio.PhotoFile == null) {
                throw new NotNullException("PhotoFile", "Null ola bilmez!");
            }

           if(!portfolio.PhotoFile.ContentType.Contains("image/"))
            {
                throw new FileContentTypeException("PhotoFile", "File tipi dogru deyil!");
            }
           if(portfolio.PhotoFile.Length>2097152)
            {
                throw new FileSizeException("PhotoFile", "File olcusu boyukdur!");
            }
           string filename=portfolio.PhotoFile.FileName;
            string path = _webHostEnvironment.WebRootPath + @"\Upload\Portfolio\" + filename;
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                portfolio.PhotoFile.CopyTo(fileStream);
            }
            portfolio.ImgUrl = filename;
            _portfolioRepository.Add(portfolio);
            _portfolioRepository.Commit();
        }

        public void Delete(int id)
        {
            var portfolio=_portfolioRepository.Get(x=> x.Id == id);
            if(portfolio == null)
            {
                throw new NotFoundException("Bele bir obyekt tapilmadi!");
            }
            string path = _webHostEnvironment.WebRootPath + @"\Upload\Portfolio\" + portfolio.ImgUrl;
            FileInfo fileInfo = new FileInfo(path);
            if(fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            _portfolioRepository.Delete(portfolio);
            _portfolioRepository.Commit();
        }

        public List<Portfolio> GetAllPortfolios(Func<Portfolio, bool>? func = null)
        {
            return _portfolioRepository.GetAll(func);
        }

        public Portfolio GetPortfolio(Func<Portfolio, bool>? func = null)
        {
            return _portfolioRepository.Get(func);
        }

        public void Update(int id, Portfolio portfolio)
        {
             var updatePortfolio=_portfolioRepository.Get(x=> x.Id == id);
            if (updatePortfolio == null)
            {
                throw new NotFoundException("Bele bir obyekt tapilmadi!");
            }
            if(portfolio.PhotoFile != null)
            {
                if (!portfolio.PhotoFile.ContentType.Contains("image/"))
                {
                    throw new FileContentTypeException("PhotoFile", "File tipi dogru deyil!");
                }
                if (portfolio.PhotoFile.Length > 2097152)
                {
                    throw new FileSizeException("PhotoFile", "File olcusu boyukdur!");
                }
                string filename = portfolio.PhotoFile.FileName;
                string path = _webHostEnvironment.WebRootPath + @"\Upload\Portfolio\" + filename;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    portfolio.PhotoFile.CopyTo(fileStream);
                }
                updatePortfolio.ImgUrl = filename;
            }
            else
            {
                portfolio.ImgUrl=updatePortfolio.ImgUrl;
            }
            updatePortfolio.Title=portfolio.Title;
            updatePortfolio.SubTitle=portfolio.SubTitle;
            _portfolioRepository.Commit();

        }
    }
}

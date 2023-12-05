using Microsoft.EntityFrameworkCore;
using powerpage.DataLayer.Context;
using powerpage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powerpage.Services;

public class LocalizationService : Service<StringResource>, ILocalizationService
{

    private readonly DbSet<StringResource> _stringResource;
    private readonly IUnitOfWork _uow;

    public LocalizationService(IUnitOfWork uow, IRepositoryAsync<StringResource> repository) : base(repository)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));


        _stringResource = uow.Set<StringResource>();
    }

    public async Task<StringResource> GetStringResource(string name, string value, int languageId)
    {
        var strres = _stringResource.FirstOrDefault(x => x.Name == name && x.LanguageId == languageId);


        if (strres == null)
        {
            strres = new StringResource() { LanguageId = languageId, Name = name, Value = value };
            await _stringResource.AddAsync(strres);
            await _uow.SaveChangesAsync();

        }
        return strres;
    }



    public async Task InsertDefaultResource(int languageId, string direction = "ltr")
    {

        var _list = new List<StringResource>();
        _list.Add(new StringResource() { LanguageId = languageId, Name = "Direction", Value = direction });

        ///TODO



        foreach (var item in _list)
        {
            var existingObj = _stringResource.FirstOrDefault(c => c.Name == item.Name && c.LanguageId == languageId);
            if (existingObj == null)
            {
                await _stringResource.AddAsync(item);

            }
        }

        await _uow.SaveChangesAsync();


    }
}

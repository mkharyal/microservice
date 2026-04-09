using PlatformService.Models;

namespace PlatformService.Data;

class PlatformRepo(AppDbContext context) : IPlatformRepo
{
    private readonly AppDbContext _context = context;

    public void CreatePlatform(Platform plat)
    {
        if (plat == null)
        {
            throw new ArgumentNullException(nameof(plat));
        }

        _context.Platforms.Add(plat);
    }

    public IEnumerable<Platform> GetAllPlatforms() => [.. _context.Platforms];
    
    public Platform GetPlatformById(int id) => _context.Platforms.FirstOrDefault(p => p.Id == id)!;

    public bool SaveChanges() => _context.SaveChanges() >= 0;

}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace PrismSample.Services
{
    public class NugetPackageService : INugetPackageService
    {
        private ISourceRepositoryProvider _sourceRepositoryProvider;

        public async Task<IEnumerable<IPackageSearchMetadata>> SearchNuGetPackages(string term, CancellationToken token)
        {
            var providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3()); // Add v3 API support
            var package = new PackageSource("https://api.nuget.org/v3/index.json");
            var source = new SourceRepository(package, providers);

            var filter = new SearchFilter(false);
            var resource = await source.GetResourceAsync<PackageSearchResource>(token).ConfigureAwait(false);
            return await resource.SearchAsync(term, filter, 0, 10, null, token).ConfigureAwait(false);
        }

        public async Task THing()
        {
            List<Lazy<INuGetResourceProvider>> providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3());  // Add v3 API support
            PackageSource packageSource = new PackageSource("https://api.nuget.org/v3/index.json");
            SourceRepository sourceRepository = new SourceRepository(packageSource, providers);
            PackageMetadataResource packageMetadataResource = await sourceRepository.GetResourceAsync<PackageMetadataResource>();
            IEnumerable<IPackageSearchMetadata> searchMetadata = await packageMetadataResource.GetMetadataAsync("Wyam.Core", true, true, null, CancellationToken.None);
        }
    }
}
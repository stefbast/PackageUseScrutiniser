using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Moq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PackageUseScrutiniser.Core.Tests.PackageFinderTests
{
    [Binding]
    public class GettingPackagesSteps
    {
        private class PackageConfig
        {
            public string FileName { get; set; }
            public string Content { get; set; }            
        }

        private class PackageConfigResult
        {
            public string PackageFileName { get; set; }            
        }

        private PackageFinder _sut;
        private IEnumerable<string> _packages;
        private string _searchPath = "searchPath";

        [Given(@"There are packages\.config files with content")]
        public void GivenThereArePackages_ConfigFilesWithContent(Table table)
        {
            var packageConfigs = table.CreateSet<PackageConfig>();

            var fileFinderMock = new Mock<IFileFinder>();            
            fileFinderMock
                .Setup(finder => finder.GetFiles(_searchPath, "packages.config"))
                .Returns(packageConfigs.Select(file => file.FileName));

            var xmlReaderMock = new Mock<IXmlReader>();
            xmlReaderMock
                .Setup(reader => reader.Read(It.IsAny<string>()))
                .Returns<string>((fileName) =>
                {
                    return XDocument.Parse(packageConfigs.Single(packageConfig => packageConfig.FileName == fileName).Content);
                });            
           
            _sut = new PackageFinder(fileFinderMock.Object, xmlReaderMock.Object);
        }
        
        [When(@"getting packages with ""(.*)""")]
        public void WhenGettingPackagesWith(string packageId)
        {
            _packages = _sut.GetPackages(packageId, _searchPath);
        }
        
        [Then(@"return packages containing package id")]
        public void ThenReturnPackagesContainingPackageId(Table table)
        {
            table.CompareToSet(_packages.Select(p => new PackageConfigResult{ PackageFileName = p}));            
        }
    }
}

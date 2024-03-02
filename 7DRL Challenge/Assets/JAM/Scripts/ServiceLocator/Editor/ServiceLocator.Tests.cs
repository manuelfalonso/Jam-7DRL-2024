using System;
using NUnit.Framework;
using UnityEngine;
using UnityServiceLocator;

public class ServiceManagerTests
{
    [Test]
    public void RegisterAndRetrieveService()
    {
        var serviceManager = new ServiceManager();
        var sampleService = new SampleService();

        serviceManager.Register(sampleService);

        Assert.IsTrue(serviceManager.TryGet(out SampleService retrievedService));
        Assert.IsNotNull(retrievedService);
        Assert.AreEqual(sampleService, retrievedService);
    }

    [Test]
    public void RegisterAndRetrieveServiceWithInterface()
    {
        var serviceManager = new ServiceManager();
        var sampleService = new SampleService();

        serviceManager.Register<ISampleService>(sampleService);

        Assert.IsTrue(serviceManager.TryGet(out ISampleService retrievedService));
        Assert.IsNotNull(retrievedService);
        Assert.AreEqual(sampleService, retrievedService);
    }

    [Test]
    public void RegisterDuplicateService()
    {
        var serviceManager = new ServiceManager();
        var sampleService = new SampleService();

        serviceManager.Register(sampleService);

        // Attempting to register the same service type again should fail
        Assert.Throws<ArgumentException>(() => serviceManager.Register(sampleService));
    }

    [Test]
    public void RetrieveNonexistentService()
    {
        var serviceManager = new ServiceManager();

        // Attempting to retrieve a service that hasn't been registered should fail
        Assert.Throws<ArgumentException>(() => serviceManager.Get<SampleService>());
    }
}

public class SampleService : ISampleService
{
    // Implementation of your sample service
}

public interface ISampleService
{
    // Interface definition for your sample service
}
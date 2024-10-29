return await Bootstrapper
  .Factory
  .CreateDocs(args)
  .AddSourceFiles()
  .RunAsync();

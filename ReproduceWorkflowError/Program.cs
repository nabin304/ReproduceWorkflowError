using ReproduceWorkflowError.Workflow;
using ReproduceWorkflowError.Workflow.Steps;
using Serilog;
using WorkflowCore.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, lc) =>
{
    lc.ReadFrom.Configuration(context.Configuration);
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWorkflow();
builder.Services.AddTransient<ProcessEvenNumber>();
builder.Services.AddTransient<ProcessOddNumber>();
builder.Services.AddTransient<EvaluateInput>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var host = app.Services.GetRequiredService<IWorkflowHost>();
host?.RegisterWorkflow<EvaluationWorkflow, EvaluationContext>();
host?.Start();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();




app.Run();



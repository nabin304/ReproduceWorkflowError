using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ReproduceWorkflowError.Workflow.Steps;

public class DecrementNumber : StepBodyAsync
{
    public int InputNumber { private get; set; }


    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        await Task.Yield();
        InputNumber--;
        return ExecutionResult.Next();
    }
}
using Quartz;
using Quartz.Impl;

namespace Job.Producer
{
    public static class SchedulerHelper
    {
        public static async void SchedulerSetup()
        {
            var _scheduler = await new StdSchedulerFactory().GetScheduler();
            await _scheduler.Start();

            var showDateTimeJob = JobBuilder.Create<TasksJob>()
                .WithIdentity("TasksJob")
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity("TasksJob")
                .StartNow()
                .WithSimpleSchedule(builder => builder.WithIntervalInMinutes(1).RepeatForever()) //.WithCronSchedule("*/1 * * * *")
                .Build();

            var result = await _scheduler.ScheduleJob(showDateTimeJob, trigger);
        }
    }
}

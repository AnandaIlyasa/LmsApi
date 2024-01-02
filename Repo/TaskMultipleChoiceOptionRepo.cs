﻿using LmsApi.Config;
using LmsApi.IRepo;
using LmsApi.Model;

namespace LmsApi.Repo;

public class TaskMultipleChoiceOptionRepo : ITaskMultipleChoiceOptionRepo
{
    readonly DBContextConfig _context;

    public TaskMultipleChoiceOptionRepo(DBContextConfig context)
    {
        _context = context;
    }

    public TaskMultipleChoiceOption CreateOption(TaskMultipleChoiceOption option)
    {
        _context.TaskMultipleChoiceOptions.Add(option);
        _context.SaveChanges();
        return option;
    }

    public List<TaskMultipleChoiceOption> GetMultipleChoiceOptionListByQuestion(int questionId)
    {
        var optionList = _context.TaskMultipleChoiceOptions
                        .Where(mco => mco.QuestionId == questionId)
                        .ToList();
        return optionList;
    }
}

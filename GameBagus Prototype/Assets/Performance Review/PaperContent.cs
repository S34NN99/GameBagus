using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paper/Content")]
public class PaperContent : ScriptableObject
{
    [SerializeField] private string _projectName;
    public string ProjectName => _projectName;

    [SerializeField] private Sprite _appImage;
    public Sprite AppImage => _appImage;

    [TextArea(5, 10)]
    [SerializeField] private string _content;
    public string Content => _content;

    [TextArea(1, 5)]
    [SerializeField] private string _signature;
    public string Signature => _signature;

    [Space]
    [Header("Page 2")]
    [SerializeField] private PerformanceReviewIcon _milestoneIcon;
    public PerformanceReviewIcon MilesteonIcon => _milestoneIcon;

    [SerializeField] private PerformanceReviewIcon _favourIcon;
    public PerformanceReviewIcon FavourIcon => _favourIcon;

    [SerializeField] private PerformanceReviewIcon _candleIcon;
    public PerformanceReviewIcon CandleIcon => _candleIcon;


}

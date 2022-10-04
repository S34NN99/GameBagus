using System.Collections.Generic;
using System.Linq;

public static class MyMaths {
    public enum ComparisonEquation {
        Greater_Than,
        Greater_Than_Equal,
        Equal,
        Smaller_Than,
        Smaller_Than_Equal,
    }

    public static bool CompareIntegers(ComparisonEquation equation, int leftOperand, int rightOperand) => equation switch {
        ComparisonEquation.Greater_Than => leftOperand > rightOperand,
        ComparisonEquation.Greater_Than_Equal => leftOperand >= rightOperand,
        ComparisonEquation.Equal => leftOperand == rightOperand,
        ComparisonEquation.Smaller_Than => leftOperand < rightOperand,
        ComparisonEquation.Smaller_Than_Equal => leftOperand <= rightOperand,
        _ => false,
    };

    public static bool CompareFloats(ComparisonEquation equation, float leftOperand, float rightOperand) => equation switch {
        ComparisonEquation.Greater_Than => leftOperand > rightOperand,
        ComparisonEquation.Greater_Than_Equal => leftOperand >= rightOperand,
        ComparisonEquation.Equal => leftOperand == rightOperand,
        ComparisonEquation.Smaller_Than => leftOperand < rightOperand,
        ComparisonEquation.Smaller_Than_Equal => leftOperand <= rightOperand,
        _ => false,
    };
}

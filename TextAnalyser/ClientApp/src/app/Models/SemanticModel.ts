import { SemanticCore } from "./SemanticCore";
import { Readability } from "./Readability";

export interface SemanticModel {
    charCount: number,
    charCountWithoutSpaces: number,
    wordCount: number,
    unicWordCount: number,
    significantWordCount: number,
    stopWordCount: number,
    senctenceCount: number,
    waterPercentage: number,
    grammaticalErrorsCount: number,
    clasicNauseaPercentage: number,
    academicalNauseaPercentage: number,
    polysyllables: number,
    readability: Readability,
    semanticCore: SemanticCore[],
    words: SemanticCore[],
    stopWords: SemanticCore[]
}
export interface LanguageToolResponseModel {
    software: Software;
    language: Language;
    matches:  Match[];
}

export interface Language {
    name:             string;
    code:             string;
    detectedLanguage: DetectedLanguage;
}

export interface DetectedLanguage {
    name:       string;
    code:       string;
    confidence: number;
}

export interface Match {
    message:      string;
    shortMessage: string;
    offset:       number;
    length:       number;
    replacements: Replacement[];
    context:      Context;
    sentence:     string;
    rule:         Rule;
}

export interface Context {
    text:   string;
    offset: number;
    length: number;
}

export interface Replacement {
    value: string;
}

export interface Rule {
    id:          string;
    subId:       string;
    description: string;
    urls:        Replacement[];
    issueType:   string;
    category:    Category;
}

export interface Category {
    id:   string;
    name: string;
}

export interface Software {
    name:       string;
    version:    string;
    buildDate:  string;
    apiVersion: number;
    status:     string;
    premium:    boolean;
}

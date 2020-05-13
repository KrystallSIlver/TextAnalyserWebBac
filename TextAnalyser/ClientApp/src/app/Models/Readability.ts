import { ReadabilityBase } from "./ReadabilityBase";

export interface Readability {
    fleschKincaid:ReadabilityBase
    daleChall: ReadabilityBase
    colemanLiau:ReadabilityBase
    smog: ReadabilityBase
    ari: ReadabilityBase
}
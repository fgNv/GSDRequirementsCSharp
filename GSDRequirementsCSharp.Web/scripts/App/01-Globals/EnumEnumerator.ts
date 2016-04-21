module Globals {
   
    function getValues(e: any) {
        return Object.keys(e).map(v => parseInt(v, 10)).filter(v => !isNaN(v));
    }

    export function enumerateEnum(e) {
        return getValues(e).map(v => {
            return { label: Sentences[e[v] as string], value: v, key: e[v] as string };
        });
    }
}
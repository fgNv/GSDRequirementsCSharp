module Views {

    declare var $
    declare var V

    export function removeAllLinks() {
        $("g.link").each((l): void => {
            var vectorized = V(l)
            vectorized.remove()
        })
    }
}
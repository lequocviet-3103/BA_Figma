export function convertHtmlToFigma(html){

  const parser = new DOMParser()
  const doc = parser.parseFromString(html,"text/html")

  const walk = (node)=>{

    const tag = node.tagName?.toLowerCase()

    if(tag==="h1" || tag==="h2" || tag==="p"){

      return {
        type:"text",
        value:node.textContent,
        fontSize:tag==="h1"?32:18
      }

    }

    if(tag==="button"){

      return {
        type:"button",
        value:node.textContent,
        width:160,
        height:44
      }

    }

    if(tag==="input"){

      return {
        type:"input",
        placeholder:node.placeholder || ""
      }

    }

    if(tag==="div" || tag==="section"){

      return {
        type:"frame",
        layout:"vertical",
        children:[...node.children].map(walk)
      }

    }

    return null

  }

  return {
    type:"frame",
    layout:"vertical",
    children:[...doc.body.children].map(walk)
  }

}
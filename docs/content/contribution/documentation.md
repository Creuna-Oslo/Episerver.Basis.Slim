# Writing Documentation

When contributing to the documentation, a few ways to do it comes to mind:

* You are contributing a new feature and writing documentation for it
* You are correcting spelling mistakes, clearing up ambiguous wording or improving the flow of the text
* You noticed the documentation is lacking for an existing feature

Or, a totally different reason I didn't think of brought you here. Regardless of the reason, here is how to write documentation:

## Getting it up and running

Documentation is written in markdown and can be found in the docs-folder.
To start writing documentation, run build.cmd from repository root with the build target **WriteDocumentation**

```bash
build.cmd WriteDocumentation
```

The script will convert all markdown files to html and put them in the output folder. Open one of them in a browser to see the result.
If the script detects a change in one of the markdown-files it will regenerate the corresponding html-file.
You must manually reload the browser to see the changes.

The script uses [FSharp.Formatting](https://tpetricek.github.io/FSharp.Formatting/) to generate html. More complete documentation of the capabilities can be found there, but here is a short reference list:

## Linking to other documents

When linking to other documents in the same documentation, use the relative path to the file from the current document and substitute .md with .html.

```md
[Link to document at same level](document.html)
```

```md
[Link to document in a subfolder](subfolder/document.html)
```

```md
[Link to document higher in the structure](../document.html)
```

## Mixing html and markdown

<strong>You can mix in html markup in the markdown</strong> (just look at the source .md for this section)
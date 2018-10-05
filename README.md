# Scribe
## A .NET File Writer 

## Getting Started
  Scribe uses C# attributes to denote a file structure in code form. The structure is then translated into TextWriter that can be used to write to disk.

  To use Scribe, affix your class properties with the FixedLengthFieldAttribute.
```
[FixedLengthField(0, 20, Alignment.Left, Padding.Zeros)]`
        `public int BusinessId { get; set; }
```
### Options for FixedLengthFile Attribute

 There are a number of options available to you for constructing a fixed length field. At minimum it requires an ordering and field length. If you do not supply the other parameters, default values will be used. Below are some of the other options available:
 
 ```
 //Alignment will be defaulted to Left and Padding will default to Spaces
 [FixedLengthField(1, 20)]`
 ```
 
 ```
 //If no custom char is provided, a space will be used by default
 [FixedLengthField(0, 20, Alignment.Left, Padding.Custom, 'X')]`
 ```
  FixedLengthField attribute allows for custom padding and the ability to pass a specific char for padding purposes.
  
  ```
   [FixedLengthField(1, 20, Alignment.Right, Padding.Spaces)]`
  ```
  FixedLengthField attribute allows users to define if a field is right or left justified. 
  
  ```
  [FixedLengthField(1, 20, Alignment.Right, Padding.Zeros)]`
  ```
  FixedLengthField attribute allows for users to pad spaces or zeros if needed.
  

### Markdown

Markdown is a lightweight and easy-to-use syntax for styling your writing. It includes conventions for

```markdown
Syntax highlighted code block

# Header 1
## Header 2
### Header 3

- Bulleted
- List

1. Numbered
2. List

**Bold** and _Italic_ and `Code` text

[Link](url) and ![Image](src)
```

For more details see [GitHub Flavored Markdown](https://guides.github.com/features/mastering-markdown/).

### Jekyll Themes

Your Pages site will use the layout and styles from the Jekyll theme you have selected in your [repository settings](https://github.com/elliotschroeder/Scribe/settings). The name of this theme is saved in the Jekyll `_config.yml` configuration file.

### Support or Contact

Having trouble with Pages? Check out our [documentation](https://help.github.com/categories/github-pages-basics/) or [contact support](https://github.com/contact) and we’ll help you sort it out.

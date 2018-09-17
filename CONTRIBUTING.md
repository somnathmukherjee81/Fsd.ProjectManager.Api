# How to contribute

We love pull requests. And following this simple guidelines will make your pull request easier to merge.

## Commit Message Guidelines

We have very precise rules over how our git commit messages can be formatted. This leads to **more
readable messages** that are easy to follow when looking through the **project history**. But also,
we use the git commit messages to **generate the project CHANGELOG**.

### Commit Message Format

Each commit message consists of a **header**, a **body** and a **footer**. The header has a special
format that includes a **type**, a **scope** and a **subject**:

Template:

```
<type>(<scope>): <subject>
<BLANK LINE>
<body>
<BLANK LINE>
<footer>
```

The **header** is mandatory and the **scope** of the header is optional.

Any line of the commit message cannot be longer 100 characters! This allows the message to be easier
to read on Repository as well as in various git tools.

Footer should contain a [closing reference to an issue](https://help.github.com/articles/closing-issues-via-commit-messages/) if any.

Samples: (even more [samples](https://github.com/angular/angular/commits/master))

```
docs(changelog): update change log to beta.5
```

```
fix(release): need to depend on latest nugget package

The version in our was outdated and users need the latest of these.
```

### Revert

If the commit reverts a previous commit, it should begin with `revert:`, followed by the header of the reverted commit. In the body it should say: `This reverts commit <hash>.`, where the hash is the SHA of the commit being reverted.

### Type

Must be one of the following:

* **build**: Changes that affect the build system or external dependencies (example scopes: gulp, broccoli, npm)
* **ci**: Changes to our CI configuration files and scripts (example scopes: Travis, Circle, BrowserStack, SauceLabs)
* **docs**: Documentation only changes
* **feat**: A new feature
* **fix**: A bug fix
* **perf**: A code change that improves performance
* **refactor**: A code change that neither fixes a bug nor adds a feature
* **revert**: Reverts an existing commit
* **style**: Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc)
* **test**: Adding missing tests or correcting existing tests

### Scope

The scope should be the name of the npm package affected (as perceived by person reading changelog generated from commit messages.

The following is the list of supported scopes:
 
* **service**
* **release**
* **storage**
* **kubernetes**
* **configuration**
* **packaging**
* **changelog**
* **project-manager-api**
* **server**

### Subject

The subject contains succinct description of the change:

* use the imperative, present tense: "change" not "changed" nor "changes"
* don't capitalize first letter
* no dot (.) at the end

### Body

Just as in the **subject**, use the imperative, present tense: "change" not "changed" nor "changes".
The body should include the motivation for the change and contrast this with previous behavior.

### Footer

The footer should contain any information about **Breaking Changes** and is also the place to
reference Repository issues that this commit **Closes**.

**Breaking Changes** should start with the word `BREAKING CHANGE:` with a space or two newlines. The rest of the commit message is then used for this.

A detailed explanation can be found in this [document][commit-message-format].

## Submitting pull requests

1. Create a new branch, please don’t work in master directly.
2. Add failing tests for the change you want to make. Run tests (see below) to see the tests fail.
3. Hack on.
4. Run tests to see if the tests pass. Repeat steps 2–4 until done.
5. Update the documentation to reflect any changes if needed.
6. Push your changes and submit a pull request.

## JavaScript code style

[See here](https://github.com/airbnb/javascript).

## Other notes

* If you have commit access to repo and want to make big change or not sure about something, make a new branch and open pull request.
* Don’t commit generated files: compiled from CSS, minified JavaScript, etc.
* Don’t change version number and changelog.
* Install [EditorConfig](http://editorconfig.org/) plugin for your code editor.
* Feel free to ask us anything you need:
  * Somnath Mukherjee ([somnath.mukherjee@cognizant.com](somnath.mukherjee@cognizant.com))

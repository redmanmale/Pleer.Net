# Pleer.NET (Prostopleer) API

[![Build status](https://ci.appveyor.com/api/projects/status/1p18i08yejbimdyd/branch/master?svg=true)](https://ci.appveyor.com/project/redmanmale/pleer.net)

It's a C# wrapper for [Pleer.net](http://pleer.net) API. It takes care of all interacting with service (HTTP requests, authorization and mapping the JSON data to .NET classes).

Its only dependency is Newtonsoft.Json.

#### About Pleer.net
>In scientific language, it's called the mp3 oriented file exchange service. We decided not to bother ourselves with scientific wordings and chose a very simple name - ProstoPleer. Here you can find good music to listen to, save and exchange using external services.

## Getting started

#### Register app

To get started, you will need to register an application with Pleer.net. It will provide you with an `app_id` and an `app_key`.

* Go to [Pleer.net](http://pleer.net), log in or register and click API.
![pic 1](https://puu.sh/taVv9/b8f1cdc574.png)

* Click `Add file` to create new app. Save `app_id` and `app_key`.
![pic 2](https://puu.sh/taVxo/910da874db.png)

#### Authorize app
Before you can use any other method you should get `access_token`. Without it or with the wrong one all methods would throw `InvalidOperationException`. If it was thrown you forgot to authorize or your token expired.

#### Use any other API methods
Official documentation [page](http://pleer.net/api). Enjoy!

### Acknowledgment
Based on [ProstoWinPleer](https://github.com/crowar/ProstoWinPleer) by [crowar](https://github.com/crowar).

## Plans
* CI
* Nuget.org
* GitHub Releases
* Cross platform (Mono, Core)
* ...

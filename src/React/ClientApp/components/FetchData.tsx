import * as React from 'react';
import * as ReactDOM from 'react-dom';
import axios from 'axios';
import * as Api from './../api.generated';
import * as Models from './../models.generated'
class FetchDataProps
{
    subreddit: string = 'javascript';
}
class FetchDataState
{
    posts: any[] = [];
}
export class FetchData extends React.Component<any, any> {

    constructor(props: FetchDataProps) {
        super(props);

        this.state = {
            posts: [],
            subreddit: 'javascript'
        };
    }

    componentDidMount() {

        axios.get(`http://www.reddit.com/r/${this.state.subreddit}.json`)       
            .then(res => {
                const posts = res.data.data.children.map(obj => obj.data);
                this.setState({ posts: posts });
            });
    }
    edit()
    { }
    refresh()
    {}
    render() {

        return (
            <div>
                <h1>{`/r/${this.state.subreddit}`}</h1>
                <ul>
                    {this.state.posts.map(post =>
                        <li key={post.id}>{post.title}</li>
                    )}
                </ul>
            </div>
        );
    }
}

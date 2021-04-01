using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForumController : MonoBehaviour
{
    public InputField forumPost;
    public GameObject Post;

    private List<GameObject> Posts;

    private float lastY;
    public float startY;
    public float incrementY;

    void Start()
    {
        Posts = new List<GameObject>();
        lastY = startY;
    }

    void GetPosts()
    {
        // Get Posts from DB
    }

    public void PostMessage()
    {

        // Update DataBase
        // If successful then done

        float x = -44;
        float z = 0;
        GameObject post = Instantiate(Post, new Vector3(x, lastY, z), Quaternion.identity);
        post.transform.SetParent(GameObject.FindGameObjectWithTag("Posts").transform, false);
        lastY -= incrementY;// Should Decrement Y
        this.Posts.Add(post);

        for (int i = this.Posts.Count - 1; i > 0; --i)
        {
            GameObject curPost = this.Posts[i];
            GameObject prevPost = this.Posts[i - 1];

            curPost.transform.Find("Email").GetComponent<Text>().text = prevPost.transform.Find("Email").GetComponent<Text>().text;
            curPost.transform.Find("Text").GetComponent<Text>().text = prevPost.transform.Find("Text").GetComponent<Text>().text;
        }
        this.Posts[0].transform.Find("Email").GetComponent<Text>().text = SecurityToken.Email;
        this.Posts[0].transform.Find("Text").GetComponent<Text>().text = forumPost.text;
    }
}

[System.Serializable]
class PostData
{
    public string email;
    public string text;
}

